import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { SongService, AlertService } from '@app/_services';

@Component({ templateUrl: 'add-edit-song.component.html' })
export class AddEditSongComponent implements OnInit {
    form: FormGroup;
    id: string;
    isAddMode: boolean;
    loading = false;
    submitted = false;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private songService: SongService,
        private alertService: AlertService
    ) {}

    ngOnInit() {
        this.id = this.route.snapshot.params['id'];
        this.isAddMode = !this.id;
        
       

        this.form = this.formBuilder.group({
            songName: ['', Validators.required],
            songUrl: ['', Validators.required],
            category: ['', Validators.required],
            user:[]
        });

        if (!this.isAddMode) {
            this.songService.getById(this.id)
                .pipe(first())
                .subscribe(x => {
                    this.f.songName.setValue(x.songName);
                    this.f.songUrl.setValue(x.songUrl);
                    this.f.category.setValue(x.category.categoryName);
                });
        }
    }

    // convenience getter for easy access to form fields
    get f() { return this.form.controls; }

    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.form.invalid) {
            return;
        }

        this.loading = true;
        if (this.isAddMode) {
            this.createSong();
        } else {
            this.updateSong();
        }
    }

    private createSong() {
        this.songService.register(this.form.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Song added successfully', { keepAfterRouteChange: true });
                    this.router.navigate(['.', { relativeTo: this.route }]);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }

    private updateSong() {
        this.songService.update(this.id, this.form.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Update successful', { keepAfterRouteChange: true });
                    this.router.navigate(['..', { relativeTo: this.route }]);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }
}